import { ShapePosition } from "./ShapePosition"

export interface BaseShape {
    name: string,
    id: number,
    surface: number,
    perimeter: number;
    shapePosition: ShapePosition;
}