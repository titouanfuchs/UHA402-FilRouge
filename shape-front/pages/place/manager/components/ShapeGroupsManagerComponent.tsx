import { FC } from "react";
import useSWR, { useSWRConfig } from "swr";
import { ShapeGroup } from "../../../../interfaces/ShapeGroup";
import ShapeGroupComponent from "./ShapeGroupComponent";

const fetcher = (...args: any[]) => fetch(...args).then(res => res.json())

const ShapeGroupsManagerComponent: FC = () => {

    const { data, error, mutate } = useSWR<ShapeGroup[], string>('/shapeAPI/api/Shape/Group', fetcher);

    if (error) return <div>Failed to load</div>;
    if (!data) return <div>Loading...</div>

    return <div className="rounded-lg overflow-hidden border border-green-500 shadow-lg">
        <div className="border-b border-green-300 w-full p-2 ">
            ShapeGroups
        </div>
        <div className="w-full flex flex-wrap min-h-[20rem] p-2">
            {data.map((group: ShapeGroup) =>
                <ShapeGroupComponent shapeGroup={group}></ShapeGroupComponent>
            )}
        </div>
    </div>;
};

export default ShapeGroupsManagerComponent;