import { BaseShape } from "./BaseShape";

export interface TriangleShape extends BaseShape {
    baseLenght: number;
    sideOne: number;
    sideTwo: number;
}