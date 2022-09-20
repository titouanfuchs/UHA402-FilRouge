import { Button, Card, CardHeader, Menu, MenuHandler, MenuItem, MenuList, Typography } from "@material-tailwind/react";
import { FC, useState } from "react";
import useSWR, { useSWRConfig } from "swr";
import { ShapeGroup } from "../../../../interfaces/ShapeGroup";
import ShapeGroupComponent from "./ShapeGroupComponent";
import ShapeGroupEditor from "./ShapeGroupEditorComponent";

const fetcher = (...args: any[]) => fetch(...args).then(res => res.json())

const ShapeGroupsManagerComponent: FC = () => {

    let [editOpen, setEditOpen] = useState(false);
    const { data, error } = useSWR<ShapeGroup[], string>('/shapeAPI/api/Shape/Group', fetcher);

    if (error) return <div>Failed to load</div>;
    if (!data) return <div>Loading...</div>

    return <Card className="border border-green-500">
        <CardHeader
            variant="gradient"
            color="green"
            className="mb-4 grid h-fit place-items-center p-2"
        >
            <Typography variant="h3" color="white">
                ShapeGroups
            </Typography>

            <Menu>
                <MenuHandler>
                    <Button variant="gradient">Options</Button>
                </MenuHandler>
                <MenuList>
                    <MenuItem onClick={() => setEditOpen(true)}>Créer</MenuItem>
                </MenuList>
            </Menu>
        </CardHeader>

        <div className="w-full flex flex-wrap min-h-[20rem] p-2 space-x-5">
            <ShapeGroupEditor isOpen={editOpen} closeEvent={() => setEditOpen(false)}></ShapeGroupEditor>

            {data.map((group: ShapeGroup, index: number) =>
                <ShapeGroupComponent key={index} shapeGroup={group}></ShapeGroupComponent>
            )}
        </div>
    </Card>;
};

export default ShapeGroupsManagerComponent;