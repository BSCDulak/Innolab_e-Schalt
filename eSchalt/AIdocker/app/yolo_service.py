import torch
from ultralytics import YOLO
from typing import List, Dict


class YoloService:
    def __init__(self, model_path: str):
        print(f"Loading YOLO model from: {model_path}")
        self.model = YOLO(model_path)

    def predict(self, image_path: str) -> List[Dict]:
        results = self.model(image_path)[0]

        detections = []
        for i, box in enumerate(results.boxes):
            x1, y1, x2, y2 = box.xyxy[0].tolist()
            cls = int(box.cls[0].item())
            conf = float(box.conf[0].item())
            name = self.model.names[cls]

            detections.append({
                "id": i,
                "name": name,
                "confidence": conf,
                "xPosTopLeft": x1,
                "yPosTopLeft": y1,
                "xPosBottomRight": x2,
                "yPosBottomRight": y2
            })

        return detections
