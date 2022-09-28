import { Button, Input, Select, Option, DialogHeader, DialogBody, DialogFooter, Dialog } from "@material-tailwind/react";
import { useEffect, useState } from "react";
import { mutate } from "swr";
import { CircleShape } from "../../../../interfaces/CircleShape";
import { RectangleShape } from "../../../../interfaces/RectangleShape";
import { ShapeDTO } from "../../../../interfaces/ShapeDTO";
import { TriangleShape } from "../../../../interfaces/TriangleShape";

const fetcher = (...args: any[]) => fetch(...args).then(res => res.json())

type ShapeT = {
    isOpen: boolean;
    createMode: boolean;
    shapeID: number;
    groupID: number;
    closeEvent: any;
}

const ShapeEditor = ({ shapeID, groupID, isOpen, closeEvent, createMode }: ShapeT) => {

    const shapes = [
        { name: 'Rectangle', type: 0 },
        { name: 'Cercle', type: 1 },
        { name: 'Triangle', type: 2 },
    ]

    const [selected, setSelected] = useState(shapes[0].name)

    const [shape, setShape] = useState({
        name: "Nouvelle Forme",
        lenght: 0,
        width: 0,
        diameter: 0,
        base: 0,
        sideOne: 0,
        sideTwo: 0,
        position: { x: 0, y: 0 }
    });

    const [fetched, setFetched] = useState(false);

    useEffect(() => {
        if (isOpen) {
            console.log("Opened");
            if (!createMode) {
                fetch(`/shapeAPI/api/Shape/${shapeID}`).then((res) => res.json()).then((data: ShapeDTO) => {

                    let newShape = shape;
                    newShape.name = data.shape.name;
                    newShape.position = data.shape.shapePosition;

                    let foundType = shapes.find((s) => { return s.type === data.type; });

                    setSelected(foundType ? foundType!.name : shapes[0].name);

                    switch (data.type) {
                        case 0:
                            let rect = data.shape as RectangleShape;

                            newShape.lenght = rect.lenght;
                            newShape.width = rect.width;

                            break;

                        case 1:
                            let circ = data.shape as CircleShape;

                            newShape.diameter = circ.diameter;

                            break;

                        case 2:
                            let tri = data.shape as TriangleShape;

                            newShape.base = tri.baseLenght;
                            newShape.sideOne = tri.sideOne;
                            newShape.sideTwo = tri.sideTwo;
                            break;
                    }

                    setShape(newShape);

                    setTimeout(function () {
                        setFetched(true);
                    }, 200);
                });
            } else {
                console.log("open in create mode");
                setFetched(true);
            }
        }
    }, [shapeID, createMode, isOpen]);

    const updateShape = async () => {
        console.log("Update Shape");

        await fetch(`/shapeAPI/api/Shape/${shapeID}`, { method: 'PATCH', body: JSON.stringify(shape), headers: { 'Content-Type': 'application/json' } });
        await mutate(`/shapeAPI/api/Shape/Group/${groupID}`);

        close();
    }

    const createShape = async () => {
        let foundType = shapes.find((s) => { return s.name === selected; });

        await fetch(`/shapeAPI/api/Shape/${foundType?.type}/${groupID}`, { method: 'POST', body: JSON.stringify(shape), headers: { 'Content-Type': 'application/json' } })
        await mutate(`/shapeAPI/api/Shape/Group/${groupID}`);

        closeEvent();
    }

    const close = () => {
        setFetched(false);
        setSelected(shapes[0].name);

        setShape({
            name: "Nouvelle Forme",
            lenght: 0,
            width: 0,
            diameter: 0,
            base: 0,
            sideOne: 0,
            sideTwo: 0,
            position: {x:0, y:0}
        });

        closeEvent();
    }

    if (!fetched) {
        return (
            <Dialog open={isOpen} handler={() => close}>
                <DialogHeader>Shape Editor</DialogHeader>
                <DialogBody divider>
                    Chargement de la forme...
                </DialogBody>
            </Dialog>
        );
    }

    return <Dialog size="lg" open={isOpen} handler={() => close}>
        <DialogHeader>Shape Editor</DialogHeader>
        <DialogBody divider>
            <div className="flex flex-col space-y-5 p-2">
                <div>
                    <Input
                        label="Shape Name"
                        type="text"
                        name="ShapeName"
                        id="shapeNameInput"
                        placeholder="Nom de la forme..."
                        defaultValue={shape.name}
                        onChange={(e) => shape.name = e.target.value}
                    />
                </div>

                <Select disabled={shapeID != 0} value={selected} onChange={(e) => setSelected(e)} label="Shape Type">
                    {shapes.map((sh) => (
                        <Option key={sh.type} value={`${sh.name}`}>{sh.name}</Option>
                    ))}
                </Select>

                {selected == "Rectangle" && (
                    <div className="full flex space-x-5">
                        <Input
                            label="Longueur"
                            type="number"
                            name="RectLenght"
                            id="RectLenght"
                            placeholder="Longueur"
                            defaultValue={shape.lenght}
                            onChange={(e) => shape.lenght = Number.parseFloat(e.target.value)}
                        />

                        <Input
                            label="Largeur"
                            type="number"
                            name="RectWidth"
                            id="shapeNameInput"
                            placeholder="Largeur"
                            defaultValue={shape.width}
                            onChange={(e) => shape.width = Number.parseFloat(e.target.value)}
                        />
                    </div>
                )}

                {selected == "Cercle" && (
                    <div className="full flex space-x-5">
                        <Input
                            label="Diamètre"
                            type="number"
                            name="CircDia"
                            id="CircDia"
                            placeholder="Diamètre"
                            defaultValue={shape.diameter}
                            onChange={(e) => shape.diameter = Number.parseFloat(e.target.value)}
                        />
                    </div>
                )}

                {selected == "Triangle" && (
                    <div className="full flex space-x-5">
                        <Input
                            type="number"
                            name="TriBase"
                            id="TriBase"
                            label="Base"
                            placeholder="Base"
                            defaultValue={shape.base}
                            onChange={(e) => shape.base = Number.parseFloat(e.target.value)}
                        />

                        <Input
                            type="number"
                            name="TriSideO"
                            id="TriSideO"
                            label="Côté 1"
                            placeholder="Côté 1"
                            defaultValue={shape.sideOne}
                            onChange={(e) => shape.sideOne = Number.parseFloat(e.target.value)}
                        />

                        <Input
                            type="number"
                            name="TriSideT"
                            id="TriSideT"
                            label="Côté 2"
                            placeholder="10"
                            defaultValue={shape.sideTwo}
                            onChange={(e) => shape.sideTwo = Number.parseFloat(e.target.value)}
                        />
                    </div>
                )}

                <div className="full flex space-x-5">
                    <Input
                        label="X"
                        type="number"
                        name="ShapeX"
                        id="ShapeX"
                        defaultValue={shape.position.x}
                        onChange={(e) => shape.position.x = Number.parseFloat(e.target.value)}
                    />

                    <Input
                        label="Y"
                        type="number"
                        name="ShapeY"
                        id="ShapeY"
                        defaultValue={shape.position.y}
                        onChange={(e) => shape.position.y = Number.parseFloat(e.target.value)}
                    />
                </div>

            </div>

        </DialogBody>
        <DialogFooter className="space-x-5">
            <Button variant="gradient" color="green" onClick={() => shapeID > 0 ? updateShape() : createShape()}>Valider</Button>
            <Button variant="gradient" color="red" onClick={() => close()}>Cancel</Button>
        </DialogFooter>
    </Dialog>;
};

export default ShapeEditor;