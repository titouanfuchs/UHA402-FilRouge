import { Popover } from "@headlessui/react";
import { useState } from "react";
import useSWR from "swr";
import { BaseShape } from "../../../../interfaces/BaseShape";
import { ShapeDTO } from "../../../../interfaces/ShapeGroupDTO";
import ShapeComponent from "./ShapeComponent";
import ShapeEditor from "./ShapeEditorComponent";

const fetcher = (...args: any[]) => fetch(...args).then(res => res.json())

const ShapeManagerComponent = () => {
    let [editOpen, setEditOpen] = useState(false);
    const { data, error } = useSWR<BaseShape[], string>(`/shapeAPI/api/Shape`, fetcher);

    const [selectedID, setSelectedID] = useState(0);

    if (error) return <div>Failed to load</div>;
    if (!data) return <div>Loading...</div>;

    const openEditWithShape = (id: number) => {
        setSelectedID(id);
        setEditOpen(true);
    }

    const openEdit = () => {
        setSelectedID(0);
        setEditOpen(true);
    }

    const closeEdit = () => {
        setSelectedID(0);
        setEditOpen(false);
    }

    return <div className="rounded-lg overflow-hidden border border-green-500 shadow-lg">

        <div className="flex justify-between border-b border-green-300 w-full p-2">
            <div className="">
                Shapes
            </div>
            <div className="flex h-full flex-col justify-center">
                <Popover className="relative">
                    <Popover.Button>Options</Popover.Button>

                    <Popover.Panel className="absolute bg-white shadow-lg border p-5 z-10 w-fit h-fit flex flex-col space-y-4">
                        <button onClick={() => openEdit()}>Créer</button>
                    </Popover.Panel>
                </Popover>
            </div>
        </div>

        <div className="w-full flex flex-wrap min-h-[20rem] p-2 space-x-5">
            <ShapeEditor shapeID={selectedID} isOpen={editOpen} closeEvent={() => closeEdit()} ></ShapeEditor>
            {data.map((shape: BaseShape, index: number) =>
                <ShapeComponent openEditEvent={(id: number) => openEditWithShape(id)} key={index} shape={shape}></ShapeComponent>
            )}
        </div>
    </div>;
};

export default ShapeManagerComponent;