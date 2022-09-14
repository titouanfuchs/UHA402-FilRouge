import { Popover } from "@headlessui/react";
import { BaseShape } from "../../../../interfaces/BaseShape";

type ShapeComponentT = {
    shape: BaseShape;
}

const ShapeComponent = ({ shape }: ShapeComponentT) => {
    return (<div>
        <div className="border h-[10rem] w-[25rem] rounded-lg flex flex-col shadow-lg mb-5">
            <div className="border-b p-2 flex justify-between">
                <div>
                    {shape.id} - {shape.name}
                </div>

                <Popover className="relative">
                    <Popover.Button>Options</Popover.Button>

                    <Popover.Panel className="absolute bg-white shadow-lg border p-5 z-10 w-fit h-fit flex flex-col space-y-4">
                        <button className="">Edit</button>
                        <button className="text-red-500">Supprimer</button>
                    </Popover.Panel>
                </Popover>

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