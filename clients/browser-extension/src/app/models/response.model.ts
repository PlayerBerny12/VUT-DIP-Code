import { DetectionMethodVM } from "./detection_method.model";

export interface ResponsesVM {
    value: number | null;
    responses: ResponseVM[];
}

export interface ResponseVM {
    detectionMethod: DetectionMethodVM;
    value: number | null;
}