import { Button, Card, Navbar, Typography } from "@material-tailwind/react";
import { NextPage } from "next";
import Link from "next/link";
import { useRouter } from "next/router";
import { Fragment, useEffect, useRef, useState } from "react";
import { Shape } from "react-konva";
import useSWR, { mutate } from "swr";
import { useCanvas } from "../../../../hooks/useCanvas";
import { CircleShape } from "../../../../interfaces/CircleShape";
import { RectangleShape } from "../../../../interfaces/RectangleShape";
import { ShapeDTO } from "../../../../interfaces/ShapeDTO";
import { ShapeGroup } from "../../../../interfaces/ShapeGroup";
import { ShapePosition } from "../../../../interfaces/ShapePosition";
import { TriangleShape } from "../../../../interfaces/TriangleShape";
import ShapeEditor from "../components/ShapeEditorComponent";

const fetcher = (...args: any[]) => fetch(...args).then(res => res.json())

const Editor: NextPage = () => {

    const [isOpen, setOpen] = useState(false);
    const [selectedShape, setSelected] = useState<number>(0);
    let shapeID: number = 0; 
    const [dragging, setDrag] = useState<boolean>(false);

    const Sign = (p1x: number, p1y: number, p2x: number, p2y: number, p3x: number, p3y: number) => {

        return (p1x - p3x) * (p2y - p3y) - (p2x - p3x) * (p1y - p3y);
    }

    let ctx:any = null;

    const router = useRouter();
    const { id } = router.query;

    const [group, setGroup] = useState<ShapeGroup | null>(null);
    let canCtx: any = null;

    const [shapes, setShapes, canvasRef, width, setWidth, height, setHeight] = useCanvas();

    function ShapeClick(e: any, prevent: boolean = false) {
        if (!prevent && !dragging) {
            var x = e.pageX - canvasRef.current.offsetLeft;
            var y = e.pageY - canvasRef.current.offsetTop;

            //Recherche de toutes les formes pouvant correspondre au click
            let shapesInRange: ShapeDTO[] = [];

            if (shapes) {
                shapes.forEach(function (element: ShapeDTO) {
                    switch (element.type) {
                        case 0:
                            let rect: RectangleShape = (element.shape as unknown) as RectangleShape;

                            if (x >= rect.shapePosition.x && x <= rect.shapePosition.x + rect.width
                                && y >= rect.shapePosition.y && y <= rect.shapePosition.y + rect.lenght) {

                                shapesInRange.push(element);
                            }

                            break;
                        case 1:
                            let circ: CircleShape = (element.shape as unknown) as CircleShape;

                            if (Math.pow(x - circ.shapePosition.x, 2) + Math.pow(y - circ.shapePosition.y, 2) < Math.pow(circ.diameter, 2)) {
                                shapesInRange.push(element);
                            }
                            break;
                        case 2:
                            let tri: TriangleShape = (element.shape as unknown) as TriangleShape;

                            var R1 = tri.baseLenght, R2 = tri.sideOne, R3 = tri.sideTwo;
                            var Bx = R3, By = 0;
                            var Cx = (R2 * R1 + R3 * R3 - R1 * R1) / (2 * R3);
                            var Cy = Math.sqrt(R2 * R2 - Cx * Cx);

                            var d1, d2, d3;
                            let has_neg: Boolean;
                            let has_pos: Boolean;

                            d1 = Sign(x, y, tri.shapePosition.x, tri.shapePosition.y, tri.shapePosition.x + Bx, tri.shapePosition.y - By);
                            d2 = Sign(x, y, tri.shapePosition.x + Bx, tri.shapePosition.y - By, tri.shapePosition.x + Cx, tri.shapePosition.y - Cy);
                            d3 = Sign(x, y, tri.shapePosition.x + Cx, tri.shapePosition.y - Cy, tri.shapePosition.x, tri.shapePosition.y);

                            has_neg = (d1 < 0) || (d2 < 0) || (d3 < 0);
                            has_pos = (d1 > 0) || (d2 > 0) || (d3 > 0);

                            if (!(has_neg && has_pos)) {
                                shapesInRange.push(element);
                            }

                            break;
                    }
                });

                //Recherche de la forme étant au dessus de toutes les autres à la position du click
                let found: ShapeDTO | null = null;
                let zIndex: number = 0;

                shapesInRange.forEach((s: ShapeDTO) => {
                    if (zIndex <= s.shape.shapePosition.z) {
                        zIndex = s.shape.shapePosition.z;
                        found = s;
                    }
                });

                //Si une forme a été trouvée
                if (found) {
                    //console.log(`${found.shape.name}`);
                    setSelected(found.shape.id);

                    if (!prevent) {
                        setOpen(true);
                    } else {
                        shapeID = found.shape.id;
                    }
                }
            }
        }
        
    }

    async function ShapeDown(evt: any) {
        if (shapeID > 0) {
            var x = evt.pageX - canvasRef.current.offsetLeft;
            var y = evt.pageY - canvasRef.current.offsetTop;

            let positionUpdate: ShapePosition = {
                x: x,
                y: y,
                z: 0
            };

            await fetch(`/shapeAPI/api/Shape/SetPosition/${shapeID}`, { method: 'PATCH', body: JSON.stringify(positionUpdate), headers: { 'Content-Type': 'application/json' } });
            await mutate(`/shapeAPI/api/Shape/Group/${id}`);

            shapeID = 0;
            setSelected(0);
        }     


        setDrag(false); 
    }

    const OpenEdit = () => {
        setSelected(0);
        setOpen(true);
    }

    const { data, error } = useSWR<ShapeGroup, string>(`/shapeAPI/api/Shape/Group/${id}`, fetcher);

    useEffect(() => {
        const can = canvasRef.current;

        can!.width = can.clientWidth;
        can!.height = can.clientHeight;

        canCtx = can!.getContext("2d");

        if (id) {
            /*fetch(`/shapeAPI/api/Shape/Group/${id}`)
                .then((res) => res.json())
                .then((data) => {
                    setGroup(data);
                });*/

            mutate(`/shapeAPI/api/Shape/Group/${id}`);
        }
    }, [id])

    useEffect(() => {
        if (data) {
            setShapes([]);
            setGroup(data);
        }
    }, [data])

    useEffect(() => {
        if (group) {
            setShapes(group.alternateShapes);
        } else {
            setShapes([]);
        }
        
    }, [group])

    useEffect(() => {
        canvasRef.current.removeEventListener("click", ShapeClick);
        canvasRef.current.removeEventListener("mousedown", () => { });
        canvasRef.current.removeEventListener("mouseup", () => { });

        canvasRef.current.addEventListener('click', (e: any) => ShapeClick(e, dragging));
        canvasRef.current.addEventListener('mousedown', (e: any) => { setDrag(true); ShapeClick(e, true); })
        canvasRef.current.addEventListener('mouseup', (e:any) => { ShapeDown(e) })
    }, [shapes])

    return (
        <Fragment>
            <ShapeEditor isOpen={isOpen} closeEvent={() => setOpen(false)} shapeID={selectedShape} groupID={id || 0} createMode={selectedShape == 0}></ShapeEditor>
            <div className="p-2 flex flex-col max-h-screen space-y-5">
                <Navbar className="mx-auto max-w-screen-xl py-2 px-4 lg:px-8 lg:py-4">
                    <div className="container mx-auto flex items-center justify-between text-blue-gray-900">
                        <div className="flex space-x-5">
                            <Typography>
                                Editor {id}
                            </Typography>
                            <div className="flex space-x-5">
                                <Button variant="gradient" size="sm" className="hidden lg:inline-block" onClick={() => OpenEdit()}>
                                    <span>Ajouter une forme</span>
                                </Button>
                            </div>
                        </div>
                        <div className="space-x-5">
                            <Link href={`/place`}>
                                <Button variant="gradient" color="red" size="sm" className="hidden lg:inline-block">
                                    <span>Cancel</span>
                                </Button>
                            </Link>
                            <Button variant="gradient" color="green" size="sm" className="hidden lg:inline-block">
                                <span>Save</span>
                            </Button>
                        </div>
                    </div>
                </Navbar>
                <div className="grow flex justify-center">
                    <div className="border aspect-square w-[50rem] m-auto shadow-lg">
                        <canvas ref={canvasRef} className=" w-full h-full"></canvas>
                    </div>
                </div>
            </div>

        </Fragment>
    );
};

export default Editor;