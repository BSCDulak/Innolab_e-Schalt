from fastapi import FastAPI, UploadFile, File
from fastapi.middleware.cors import CORSMiddleware
import shutil
# import os

from .yolo_service import YoloService
from .schemas import PredictionResponse, Component
from .postprocessing import validate_layout

app = FastAPI(title="eSchalt AI")

# backend → frontend communication
app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],  
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

# Loading YOLO
yolo = YoloService("best.pt")


@app.get("/health")
def health():
    return {"status": "ok"}


@app.post("/predict", response_model=PredictionResponse)
async def predict(file: UploadFile = File(...)):
    temp_path = "/tmp/uploaded_image.jpg"

    with open(temp_path, "wb") as buffer:
        shutil.copyfileobj(file.file, buffer)

    detections = yolo.predict(temp_path)

    components = [Component(**d) for d in detections]

    import cv2
    img = cv2.imread(temp_path)
    h, w, _ = img.shape

    warnings = validate_layout(detections, w, h)

    return PredictionResponse(
        components=components,
        imageWidth=w,
        imageHeight=h,
        warnings=warnings
    )