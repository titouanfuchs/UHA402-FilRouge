import { Popover } from "@headlessui/react";
import { FC } from "react";

type headerBar = {
    Title:string,
}

const Headerbar = ({ Title }: headerBar) => {
    return <>
        <div className="h-[4rem] w-full border-b light:border-black justify-between flex p-5 mb-5 shadow-lg">
            <div className="flex h-full flex-col justify-center">
                <p className="text-3xl">
                    {Title}
                </p>
            </div>

            <div className="flex h-full flex-col justify-center">
                <Popover className="relative">
                    <Popover.Button>Options</Popover.Button>

                    <Popover.Panel className="absolute bg-white shadow-lg border p-5 z-10 w-fit h-fit flex flex-col space-y-4">
                        <button>Créer</button>
                    </Popover.Panel>
                </Popover>
            </div>
        </div>
    </>;
};

export default Headerbar;