import { Button, Card, CardHeader, MenuHandler, MenuItem, MenuList, Typography, Menu } from "@material-tailwind/react";
import { useState } from "react";
import useSWR from "swr";
import { BaseShape } from "../../../../interfaces/BaseShape";
import { ShapesDTO } from "../../../../interfaces/ShapeGroupDTO";
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
        setEditOpen(false);
    }

    return <Card className="border border-green-500">
        <CardHeader
            variant="gradient"
            color="green"
            className="mb-4 grid h-fit place-items-center p-2"
        >
            <Typography variant="h3" color="white">
                Shapes
            </Typography>

            <Menu>
                <MenuHandler>
                    <Button variant="gradient">Options</Button>
                </MenuHandler>
                <MenuList>
                    <MenuItem onClick={() => openEdit()}>Créer</MenuItem>
                </MenuList>
            </Menu>
        </CardHeader>

        <div className="w-full flex flex-wrap min-h-[20rem] p-2 space-x-5">
            <ShapeEditor shapeID={selectedID} isOpen={editOpen} createMode={selectedID == 0} closeEvent={() => closeEdit()} ></ShapeEditor>
            {data.map((shape: BaseShape, index: number) =>
                <ShapeComponent openEditEvent={(id: number) => openEditWithShape(id)} key={index} shape={shape}></ShapeComponent>
            )}
        </div>
    </Card>;
};

export default ShapeManagerComponent;