from pydantic import BaseModel
from typing import List, Tuple


class Component(BaseModel):
    id: int
    name: str
    confidence: float
    xPosTopLeft: float
    yPosTopLeft: float
    xPosBottomRight: float
    yPosBottomRight: float


class PredictionResponse(BaseModel):
    components: List[Component]
    imageWidth: int
    imageHeight: int
