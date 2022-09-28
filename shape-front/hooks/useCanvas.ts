import { useEffect, useRef, useState } from "react";
import { CircleShape } from "../interfaces/CircleShape";
import { RectangleShape } from "../interfaces/RectangleShape";
import { ShapeDTO } from "../interfaces/ShapeDTO";
import { TriangleShape } from "../interfaces/TriangleShape";



export function draw(ctx: any, shape: ShapeDTO) {
    ctx.beginPath();
    if (shape.type == 0) {

        let rect: RectangleShape = (shape.shape as unknown) as RectangleShape;

        ctx.fillStyle = "red";
        ctx.fillRect(rect.shapePosition.x, rect.shapePosition.y, rect.width, rect.lenght);
    }

    if (shape.type == 1) {
        let circ: CircleShape = (shape.shape as unknown) as CircleShape;

        ctx.beginPath();
        ctx.arc(circ.shapePosition.x, circ.shapePosition.y, circ.diameter, 0, 2 * Math.PI, false);
        ctx.fillStyle = "blue";
        ctx.fill();
    }

    if (shape.type == 2) {
        let tri: TriangleShape = (shape.shape as unknown) as TriangleShape;

        var R1 = tri.baseLenght, R2 = tri.sideOne, R3 = tri.sideTwo;
        var Bx = R3, By = 0;
        var Cx = (R2 * R1 + R3 * R3 - R1 * R1) / (2 * R3);
        var Cy = Math.sqrt(R2 * R2 - Cx * Cx);

        ctx.beginPath();
        ctx.moveTo(tri.shapePosition.x, tri.shapePosition.y);
        ctx.lineTo(tri.shapePosition.x + Bx, tri.shapePosition.y - By);
        ctx.lineTo(tri.shapePosition.x + Cx, tri.shapePosition.y - Cy);
        ctx.closePath();
        ctx.fillStyle = "gold";
        ctx.fill();
    }


    ctx.closePath();
    ctx.restore();
}

export function useCanvas() {
    const canvasRef = useRef(null);
    const [shapes, setShapes] = useState([]);
    const [width, setWidth] = useState(0);
    const [height, setHeight] = useState(0);

    useEffect(() => {
        const canvasObj = canvasRef.current;
        const ctx = canvasObj!.getContext('2d');

        ctx.clearRect(0, 0, canvasObj.width, canvasObj.height)

        if (shapes) {
            shapes.forEach((shape) => { draw(ctx, shape); })
        }

    })

    return [shapes, setShapes, canvasRef, width, setWidth, height, setHeight];
}