import { Button, Menu, MenuHandler, MenuItem, MenuList } from "@material-tailwind/react";
import { useState } from "react";
import { useSWRConfig } from "swr";
import { ShapeGroup } from "../../../../interfaces/ShapeGroup";
import ShapeGroupEditor from "./ShapeGroupEditorComponent";

type ShapeGroupC = {
    shapeGroup: ShapeGroup;
    openAllocation: any
}

const ShapeGroupComponent = ({ shapeGroup, openAllocation }: ShapeGroupC) => {

    let [editOpen, setEditOpen] = useState(false);

    const { mutate } = useSWRConfig();

    const deleteGroup = async () => {
        await fetch(`/shapeAPI/api/Shape/Group/${shapeGroup.id}`, { method: 'DELETE' });
        await mutate('/shapeAPI/api/Shape/Group');
    }

    return (
        <div>
            <ShapeGroupEditor isOpen={editOpen} shapeGroup={shapeGroup} closeEvent={() => setEditOpen(false)}></ShapeGroupEditor>
            <div className="border h-[10rem] w-[25rem] rounded-lg flex flex-col shadow-lg mb-5">
                <div className="border-b p-2 flex justify-between">
                    <div>
                        {shapeGroup.id} - {shapeGroup.groupName}
                    </div>

                    <Menu>
                        <MenuHandler>
                            <Button variant="outlined">Options</Button>
                        </MenuHandler>
                        <MenuList>
                            <MenuItem onClick={() => openAllocation()}>Ajouter une forme</MenuItem>
                            <MenuItem onClick={() => setEditOpen(true)}>Edit</MenuItem>
                            <MenuItem onClick={() => deleteGroup()}>Supprimer</MenuItem>
                        </MenuList>
                    </Menu>
                </div>

                <div className="grow p-4">
                    <p>
                        Nbr de formes : {shapeGroup.shapes.length}
                    </p>
                    <p>
                        Surface totale : {shapeGroup.surface}
                    </p>
                    <p>
                        Périmètre total : {shapeGroup.perimeter}
                    </p>
                </div>
            </div>
        </div>);
};

export default ShapeGroupComponent;