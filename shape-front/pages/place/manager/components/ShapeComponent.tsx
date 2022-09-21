import { Button, Menu, MenuHandler, MenuItem, MenuList } from "@material-tailwind/react";
import { useState } from "react";
import { useSWRConfig } from "swr";
import { BaseShape } from "../../../../interfaces/BaseShape";

type ShapeComponentT = {
    shape: BaseShape;
    openEditEvent: any
}

const ShapeComponent = ({ shape, openEditEvent }: ShapeComponentT) => {
    let [editOpen, setEditOpen] = useState(false);

    const { mutate } = useSWRConfig();

    const deleteShape = async () => {
        await fetch(`/shapeAPI/api/Shape/${shape.id}`, { method: 'DELETE' });
        await mutate('/shapeAPI/api/Shape');
        await mutate('/shapeAPI/api/Shape/Group');
    }

    return (<div>
        <div className="border h-[10rem] w-[25rem] rounded-lg flex flex-col shadow-lg mb-5">
            <div className="border-b p-2 flex justify-between">
                <div>
                    {shape.id} - {shape.name}
                </div>

                <Menu>
                    <MenuHandler>
                        <Button variant="outlined">Options</Button>
                    </MenuHandler>
                    <MenuList>
                        <MenuItem onClick={() => openEditEvent(shape.id)}>Edit</MenuItem>
                        <MenuItem onClick={() => deleteShape()}>Supprimer</MenuItem>
                    </MenuList>
                </Menu>
            </div>

            <div className="grow p-4">
                <p>
                    Périmètre: {shape.perimeter}
                </p>
                <p>
                    Aire : {shape.surface}
                </p>
            </div>
        </div>
    </div>);
};

export default ShapeComponent;