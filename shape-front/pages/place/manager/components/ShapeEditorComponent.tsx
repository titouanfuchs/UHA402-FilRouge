import { Dialog, Listbox } from "@headlessui/react";
import { useState } from "react";
import { isReadable } from "stream";
import { mutate } from "swr";
import { BaseShape } from "../../../../interfaces/BaseShape";
import { CircleShape } from "../../../../interfaces/CircleShape";
import { RectangleShape } from "../../../../interfaces/RectangleShape";
import { ShapeQuery } from "../../../../interfaces/ShapeDTO";
import { TriangleShape } from "../../../../interfaces/TriangleShape";

const fetcher = (...args: any[]) => fetch(...args).then(res => res.json())

type ShapeT = {
    isOpen: boolean;
    shapeID: number;
    closeEvent: any;
}

const ShapeEditor = ({ shapeID, isOpen, closeEvent }: ShapeT) => {

    const shapes = [
        { name: 'Rectangle', type: 0 },
        { name: 'Cercle', type: 1 },
        { name: 'Triangle', type: 2 },
    ]

    const [selected, setSelected] = useState(shapes[0])

    const [shape, setShape] = useState({
        name: "Nouvelle Forme",
        lenght: 0,
        width: 0,
        diameter: 0,
        base: 0,
        sideOne: 0,
        sideTwo: 0
    });

    const [ready, setReady] = useState(false);
    const [fetched, setFetched] = useState(false);

    if (isOpen && shapeID > 0 && !fetched) {
        setFetched(true);

        fetch(`/shapeAPI/api/Shape/${shapeID}`).then((res) => res.json()).then((data: BaseShape) => {
            console.log(data);
            let newShape = shape;
            newShape.name = data.name;

            let type = shapes.map(s => s.type).find(data.shapeType);

            setSelected(type || 0)

            switch (data.shapeType) {
                case 0:
                    let rect = data as RectangleShape;

                    newShape.lenght = rect.lenght;
                    newShape.width = rect.width;

                    break;

                case 1:
                    let circ = data as CircleShape;

                    newShape.diameter = circ.diameter;
                    break;

                case 2:
                    let tri = data as TriangleShape;

                    newShape.base = tri.baseLenght;
                    newShape.sideOne = tri.sideOne;
                    newShape.sideTwo = tri.sideTwo;

                    break;
            }

            setShape(newShape);
            setReady(true);
        });
    }

    const updateShape = async () => {

    }

    const createShape = async () => {
        await fetch('/shapeAPI/api/Shape', { method: 'POST', body: JSON.stringify(shape), headers: { 'Content-Type': 'application/json' } })
        await mutate('/shapeAPI/api/Shape');

        closeEvent();
    }

    return <Dialog open={isOpen} onClose={() => closeEvent()} className="relative z-50">
        <div className="fixed inset-0 flex items-center justify-center p-4">
            <Dialog.Panel className="w-full max-w-sm rounded-lg bg-white border shadow-lg p-5">
                <Dialog.Title className="text-2xl">ShapeGroup Editor</Dialog.Title>
                <Dialog.Description>
                    Dialogue d'édition de formes
                </Dialog.Description>

                <div className="flex flex-col space-y-5 p-2">
                    <div>
                        <input
                            type="text"
                            name="ShapeName"
                            id="shapeNameInput"
                            className="block w-full rounded-md border-gray-300 pl-7  focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm"
                            placeholder="Nom de la forme..."
                            defaultValue={shape.name}
                            onChange={(e) => shape.name = e.target.value}
                        />
                    </div>

                    <Listbox value={selected} onChange={setSelected}>
                        <Listbox.Button className="relative w-full cursor-default rounded-lg bg-white py-2 pl-3 pr-10 text-left shadow-md focus:outline-none focus-visible:border-indigo-500 focus-visible:ring-2 focus-visible:ring-white focus-visible:ring-opacity-75 focus-visible:ring-offset-2 focus-visible:ring-offset-orange-300 sm:text-sm">{selected.name}</Listbox.Button>
                        <Listbox.Options className="p-2 absolute mt-1 max-h-60 min-w-[15rem] overflow-auto rounded-md bg-white py-1 text-base shadow-lg ring-1 ring-black ring-opacity-5 focus:outline-none sm:text-sm">
                            {shapes.map((sh) => (
                                <Listbox.Option
                                    key={sh.type}
                                    value={sh}
                                >
                                    {sh.name}
                                </Listbox.Option>
                            ))}
                        </Listbox.Options>
                    </Listbox>

                    {selected.type == 0 && (
                        <div className="full flex space-x-5">
                            <input
                                type="number"
                                name="RectLenght"
                                id="shapeNameInput"
                                className="block w-full rounded-md border-gray-300 pl-7  focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm"
                                placeholder="Longueur"
                                defaultValue={shape.lenght}
                                onChange={(e) => shape.lenght = Number.parseFloat(e.target.value)}
                            />

                            <input
                                type="number"
                                name="RectWidth"
                                id="shapeNameInput"
                                className="block w-full rounded-md border-gray-300 pl-7  focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm"
                                placeholder="Largeur"
                                defaultValue={shape.width}
                                onChange={(e) => shape.width = Number.parseFloat(e.target.value)}
                            />
                        </div>
                    )}

                </div>

                <div className="flex space-x-5 justify-end">
                    <button onClick={() => shapeID > 0 ? updateShape() : createShape()}>Valider</button>
                    <button onClick={() => closeEvent()} className="text-red-500">Cancel</button>
                </div>

            </Dialog.Panel>
        </div>
    </Dialog>;
};

export default ShapeEditor;