import { Button, Dialog, DialogBody, DialogFooter, DialogHeader, Select, Option } from "@material-tailwind/react";
import { useEffect, useState } from "react";
import useSWR, { mutate } from "swr";
import { BaseShape } from "../../../../interfaces/BaseShape";

type ShapeGroupAllocationT = {
    isOpen: boolean;
    closeEvent: any;
    groupID: number;
}

const fetcher = (...args: any[]) => fetch(...args).then(res => res.json())

const ShapeGroupAllocation = ({ isOpen, closeEvent, groupID }: ShapeGroupAllocationT) => {

    const { data, error } = useSWR<BaseShape[], string>(`/shapeAPI/api/Shape`, fetcher);

    if (error) return <div>Failed to load</div>;
    if (!data) return <div>Loading...</div>;

    const [selected, setSelected] = useState("0");

    const close = () => {
        closeEvent();
    };

    const save = async (shapeID: string) => {
        await fetch(`/shapeAPI/api/Shape/AddShapeToGroup/${shapeID}/${groupID}`, { method:"POST" });
        await mutate('/shapeAPI/api/Shape/Group');

        close();
    }

    return <Dialog size="md" handler={close} open={isOpen}>
        <DialogHeader>
            Ajouter une forme dans le groupe {groupID}
        </DialogHeader>
        <DialogBody divider className="min-h-[20rem]">
            <Select onChange={(e) => setSelected(e!)} label="Sélectionner une forme à associer">
                {data.map((shape: BaseShape, index: number) =>
                    <Option value={`${shape.id}`} id={index.toString()}>{shape.id} - {shape.name}</Option>
                )}
                
            </Select>
        </DialogBody>
        <DialogFooter className="space-x-5">
            <Button variant="gradient" color="green" onClick={() => save(selected)}>Ajouter</Button>
            <Button variant="gradient" color="red" onClick={() => close()}>Cancel</Button>
        </DialogFooter>
    </Dialog>;
};

export default ShapeGroupAllocation;