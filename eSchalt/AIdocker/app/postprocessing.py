from typing import List, Dict


def validate_layout(components: List[Dict], image_width: int, image_height: int) -> List[str]:
    warnings: List[str] = []

    for c in components:
        x1 = c["xPosTopLeft"]
        y1 = c["yPosTopLeft"]
        x2 = c["xPosBottomRight"]
        y2 = c["yPosBottomRight"]
        name = c["name"]
        cid = c["id"]

        if x1 < 0 or y1 < 0 or x2 > image_width or y2 > image_height:
            warnings.append(
                f"Component {cid} ('{name}') has a bounding box partially outside the image."
            )

    return warnings
