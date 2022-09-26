import { Button, Card, Navbar, Typography } from "@material-tailwind/react";
import { NextPage } from "next";
import Link from "next/link";
import { useRouter } from "next/router";
import { useEffect, useRef, useState } from "react";
import { RectangleShape } from "../../../../interfaces/RectangleShape";
import { ShapeDTO } from "../../../../interfaces/ShapeDTO";
import { ShapeGroup } from "../../../../interfaces/ShapeGroup";

const Editor: NextPage = () => {
    const canvas = useRef();
    let ctx:any = null;

    const router = useRouter();
    const { id } = router.query;

    const [group, setGroup] = useState<ShapeGroup | null>(null);

    useEffect(() => {


        if (!group) {
            fetch(`/shapeAPI/api/Shape/Group/${id}`)
                .then((res) => res.json())
                .then((data) => {
                    setGroup(data);
                });
        } else {
            const canvasEle = canvas.current;

            ctx = canvasEle!.getContext("2d");

            for (let i = 0; i < group.alternateShapes.length; i++) {

                var s = group.alternateShapes[i];
                

                if (s.type == 0) {

                    let rect: RectangleShape = (s.shape as unknown) as RectangleShape;

                    ctx.beginPath();
                    ctx.fillStyle = "red";
                    ctx.fillRect(rect.shapePosition.x, rect.shapePosition.y, rect.width, rect.lenght);
                }
            }
        }


    }, [group])

    if (group) {
        return (
            <div className="p-2 flex flex-col max-h-screen space-y-5">
                <Navbar className="mx-auto max-w-screen-xl py-2 px-4 lg:px-8 lg:py-4">
                    <div className="container mx-auto flex items-center justify-between text-blue-gray-900">
                        <Typography>
                            Editor {id}
                        </Typography>
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
                        <canvas ref={canvas}></canvas>
                    </div>
                    <Card className="border w-[30rem]">
                    </Card>
                </div>
            </div>
        );
    }

    return <div>Loading...</div>
};

export default Editor;