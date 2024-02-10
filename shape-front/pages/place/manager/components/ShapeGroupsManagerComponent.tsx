import { Button, Card, CardHeader, Menu, MenuHandler, MenuItem, MenuList, Typography } from "@material-tailwind/react";
import { FC, useState } from "react";
import useSWR, { useSWRConfig } from "swr";
import { ShapeGroup } from "../../../../interfaces/ShapeGroup";
import ShapeGroupAllocation from "./ShapeGroupAllocation";
import ShapeGroupComponent from "./ShapeGroupComponent";
import ShapeGroupEditor from "./ShapeGroupEditorComponent";

const fetcher = (...args: any[]) => fetch(...args).then(res => res.json())

const ShapeGroupsManagerComponent: FC = () => {

    let [editOpen, setEditOpen] = useState(false);
    let [allocationOpen, setAllocationOpen] = useState(false);

    const [selectedGroup, setSelectedGroup] = useState(0);

    const { data, error } = useSWR<ShapeGroup[], string>('/shapeAPI/api/Shape/Group', fetcher);

    const openAllocation = (groupID: number) => {
        setSelectedGroup(groupID);
        setAllocationOpen(true);
    }

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
            <ShapeGroupAllocation groupID={selectedGroup} isOpen={allocationOpen} closeEvent={() => setAllocationOpen(false)}></ShapeGroupAllocation>

            {data.map((group: ShapeGroup, index: number) =>
                <ShapeGroupComponent key={index} shapeGroup={group} openAllocation={() => openAllocation(group.id)}></ShapeGroupComponent>
            )}
        </div>
    </Card>;
};

export default ShapeGroupsManagerComponent;