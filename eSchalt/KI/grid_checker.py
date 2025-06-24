import os
import cv2
import numpy as np
from ultralytics import YOLO
from grid_config import GRID_ROWS, GRID_COLS, ZONE_RULES

# === Settings ===
image_path = "runs/predict/image0.jpg"   # Or dynamically list output folder
model_path = "Training-Output/experiment1/weights/best.pt"

# === Load model + image ===
model = YOLO(model_path)
results = model.predict(source=image_path, save=False)

img = cv2.imread(image_path)
img_h, img_w = img.shape[:2]
cell_h = img_h // GRID_ROWS
cell_w = img_w // GRID_COLS

# === Process each detection ===
for result in results:
    masks = result.masks.data.cpu().numpy() if result.masks is not None else []
    names = result.names
    classes = result.boxes.cls.cpu().numpy().astype(int)

    for i, mask in enumerate(masks):
        cls = classes[i]
        label = names[cls]

        # Get mask center
        ys, xs = np.where(mask > 0.5)
        if len(xs) == 0 or len(ys) == 0:
            continue
        cx, cy = int(np.mean(xs)), int(np.mean(ys))
        row, col = cy // cell_h, cx // cell_w

        # Draw center and grid position
        cv2.circle(img, (cx, cy), 5, (0, 255, 0), -1)
        cv2.putText(img, f"{label} @({row},{col})", (cx, cy - 10),
                    cv2.FONT_HERSHEY_SIMPLEX, 0.4, (255, 255, 255), 1)

        # Check if the detection is allowed in this grid cell
        if (row, col) not in ZONE_RULES.get(label, []):
            print(f"❗ ALERT: '{label}' found at ({row},{col}) — unexpected position!")

# === Draw grid overlay ===
for i in range(1, GRID_ROWS):
    y = i * cell_h
    cv2.line(img, (0, y), (img_w, y), (255, 255, 255), 1)
for j in range(1, GRID_COLS):
    x = j * cell_w
    cv2.line(img, (x, 0), (x, img_h), (255, 255, 255), 1)

# === Save / Show result ===
cv2.imshow("Grid Checker", img)
cv2.waitKey(0)
cv2.destroyAllWindows()
cv2.imwrite("grid_checked_output.jpg", img)
