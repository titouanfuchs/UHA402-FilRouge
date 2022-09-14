import { Dialog, Popover } from "@headlessui/react";
import { FC, useState } from "react";
import { mutate, useSWRConfig } from "swr";
import { ShapeGroup } from "../../../../interfaces/ShapeGroup";
import ShapeGroupEditor from "./ShapeGroupEditorComponent";

type ShapeGroupC = {
    shapeGroup: ShapeGroup;
}

const ShapeGroupComponent = ({ shapeGroup }: ShapeGroupC) => {

    let [editOpen, setEditOpen] = useState(false);

    const { mutate } = useSWRConfig();

    const deleteGroup = async () => {
        await fetch(`/shapeAPI/api/Shape/Group/${shapeGroup.id}`, { method: 'DELETE' });
        await mutate('/shapeAPI/api/Shape/Group');
    }

    return (
        <div>
            <ShapeGroupEditor isOpen={editOpen} shapeGroup={shapeGroup} closeEvent={() => setEditOpen(false)}></ShapeGroupEditor>
            <div className="border h-[10rem] w-[25rem] rounded-lg flex flex-col shadow-lg">
                <div className="border-b p-2 flex justify-between">
                    <div>
                        {shapeGroup.id} - {shapeGroup.groupName}
                    </div>

                    <Popover className="relative">
                        <Popover.Button>Options</Popover.Button>

                        <Popover.Panel className="absolute bg-white shadow-lg border p-5 z-10 w-fit h-fit flex flex-col space-y-4">
                            <button className="" onClick={() => setEditOpen(true)}>Edit</button>
                            <button className="text-red-500" onClick={() => deleteGroup()}>Supprimer</button>
                        </Popover.Panel>
                    </Popover>

                </div>

                <div className="grow p-4">
                    <p>
                        Nbr de formes : {shapeGroup.shapes.length}
                    </p>
                </div>
            </div>
        </div>);
};

export default ShapeGroupComponent;