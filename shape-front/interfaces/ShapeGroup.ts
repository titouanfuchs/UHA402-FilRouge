import { Position } from "./Position"
import { ShapeDTO } from "./ShapeDTO"

export interface ShapeGroup {
    groupName: string,
    shapes: any[],
    alternateShapes: ShapeDTO[],
    surface: number,
    perimeter: number,
    groupPosition: Position,
    owner:string,
    id:number
}