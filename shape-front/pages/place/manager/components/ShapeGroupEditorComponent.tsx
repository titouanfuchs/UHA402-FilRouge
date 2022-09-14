import { Dialog } from "@headlessui/react";
import { mutate } from "swr";
import { ShapeGroup } from "../../../../interfaces/ShapeGroup";

type ShapeGroupEditorT = {
    isOpen: boolean;
    shapeGroup?: ShapeGroup;
    closeEvent: any;
}

interface ShapeGroupDTO {
    groupName:string
}

const ShapeGroupEditor = ({ shapeGroup, isOpen, closeEvent }: ShapeGroupEditorT) => {

    let sGroup: ShapeGroup = shapeGroup ? shapeGroup! : ({ groupName: "Nouveau Groupe", shapes: [], id: 0 });

    const updateShape = async () => {
        let edit: ShapeGroupDTO = {
            groupName: sGroup.groupName
        }

        await fetch(`/shapeAPI/api/Shape/Group/${shapeGroup!.id}`, { headers: { 'Content-Type': 'application/json' }, method: 'PATCH', body: JSON.stringify(edit) });
        await mutate('/shapeAPI/api/Shape/Group');
        closeEvent();
    }

    const createShape = async () => {
        let edit: ShapeGroupDTO = {
            groupName: sGroup.groupName
        };

        await fetch(`/shapeAPI/api/Shape/Group/`, { headers: { 'Content-Type': 'application/json' }, method: 'POST', body: JSON.stringify(edit) });
        await mutate('/shapeAPI/api/Shape/Group');
        closeEvent();
    }

    return <Dialog open={isOpen} onClose={() => closeEvent()} className="relative z-50">
        <div className="fixed inset-0 flex items-center justify-center p-4">
            <Dialog.Panel className="w-full max-w-sm rounded-lg bg-white border shadow-lg p-5">
                <Dialog.Title className="text-2xl">ShapeGroup Editor</Dialog.Title>
                <Dialog.Description>
                    Dialogue d'édition de ShapesGroupe
                </Dialog.Description>

                <div className="flex flex-col space-y-5 p-2">
                    <div>
                        <input
                            type="text"
                            name="GroupName"
                            id="groupNameInput"
                            className="block w-full rounded-md border-gray-300 pl-7  focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm"
                            placeholder="Nom de groupe"
                            defaultValue={sGroup.groupName}
                            onChange={(e) => sGroup.groupName = e.target.value}
                        />
                    </div>
                </div>

                <div className="flex space-x-5 justify-end">
                    <button onClick={() => shapeGroup ? updateShape() : createShape()}>Valider</button>
                    <button onClick={() => closeEvent()} className="text-red-500">Cancel</button>
                </div>

            </Dialog.Panel>
        </div>
    </Dialog>;
};

export default ShapeGroupEditor;