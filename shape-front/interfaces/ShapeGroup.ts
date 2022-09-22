import { Position } from "./Position"

export interface ShapeGroup {
    groupName: string,
    shapes: any[],
    surface: number,
    perimeter: number,
    groupPosition: Position,
    owner:string,
    id:number
}