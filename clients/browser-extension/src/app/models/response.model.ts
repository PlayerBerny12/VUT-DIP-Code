import { DetectionMethodVM } from "./detection_method.model";

export interface ResponsesVM {
    value: number;
    responses: ResponseVM[];
}

export interface ResponseVM {
    detectionMethod: DetectionMethodVM;
    value: number;
}