import { Dialog, DialogBody, DialogHeader, Input, Select, Option, DialogFooter, Button } from "@material-tailwind/react";
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

    const closeDialog = () => {
        closeEvent();
    }

    return (
        <Dialog open={isOpen} handler={() => closeDialog()}>
            <DialogHeader>ShapeGroup Editor</DialogHeader>
            <DialogBody divider>
                <div className="flex flex-col space-y-5 p-2">
                    <div>
                        <Input
                            label="Shape Name"
                            type="text"
                            name="ShapeName"
                            id="shapeNameInput"
                                placeholder="Nom du groupe..."
                                defaultValue={sGroup.groupName}
                                onChange={(e) => sGroup.groupName = e.target.value}
                        />
                    </div>
                </div>

            </DialogBody>
            <DialogFooter className="space-x-5">
                <Button variant="gradient" color="green" onClick={() => shapeGroup != null ? updateShape() : createShape()}>Valider</Button>
                <Button variant="gradient" color="red" onClick={() => closeDialog()}>Cancel</Button>
            </DialogFooter>
        </Dialog>
    );
};

export default ShapeGroupEditor;