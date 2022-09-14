import { Popover } from "@headlessui/react";
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

    return <div className="rounded-lg overflow-hidden border border-green-500 shadow-lg">
        <div className="flex justify-between">
            <div className="border-b border-green-300 w-full p-2">
                ShapeGroups
            </div>
            <div className="flex h-full flex-col justify-center">
                <Popover className="relative">
                    <Popover.Button>Options</Popover.Button>

                    <Popover.Panel className="absolute bg-white shadow-lg border p-5 z-10 w-fit h-fit flex flex-col space-y-4">
                        <button onClick={() => setEditOpen(true)}>Créer</button>
                    </Popover.Panel>
                </Popover>
            </div>
        </div>

        <div className="w-full flex flex-wrap min-h-[20rem] p-2">
            <ShapeGroupEditor isOpen={editOpen} closeEvent={() => setEditOpen(false)}></ShapeGroupEditor>

            {data.map((group: ShapeGroup, index: number) =>
                <ShapeGroupComponent key={index} shapeGroup={group}></ShapeGroupComponent>
            )}
        </div>
    </div>;
};

export default ShapeGroupsManagerComponent;