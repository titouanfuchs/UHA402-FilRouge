import { Menu, MenuHandler, MenuItem, MenuList, Tooltip } from '@material-tailwind/react';
import type { NextPage } from 'next';
import Link from 'next/link';
import { useEffect } from 'react';
import useSWR from 'swr';
import { ShapeGroup } from '../../interfaces/ShapeGroup';

const fetcher = (...args: any[]) => fetch(...args).then(res => res.json())

const Place: NextPage = () => {

    const { data, error } = useSWR<ShapeGroup[], string>(`/shapeAPI/api/Shape/Group`, fetcher);

    useEffect(() => {

        if (data) {
            if (data.length > 0)
                document.getElementById("PlaceCanvas")!.style.width = `${5 * (data[data.length - 1].groupPosition.x + 1) + 1.3}rem`;
        }

    })

    if (error) return <div>Failed to load</div>;
    if (!data) return <div>Loading...</div>;

    return (
        <div id="PlaceCanvas" className="flex flex-wrap justify-center border border-black p-2 m-auto">
            {data.map((group: ShapeGroup, index: number) =>
                <Menu key={group.groupName}>
                    <MenuHandler>
                        <div className="aspect-square border w-20 hover:scale-150 hover:z-[10] transition-all bg-white hover:shadow-lg ease-in-out cursor-pointer">
                            {group.id}
                        </div>
                    </MenuHandler>
                    <MenuList>
                        <MenuItem disabled>Voter pour la suppression</MenuItem>
                        <MenuItem disabled>Visiter</MenuItem>
                        <Link href={`/place/editor/${group.id}`}>
                            <MenuItem disabled={group.owner != "EVERY"}>Cette case est pour moi !</MenuItem>
                        </Link>
                    </MenuList>
                </Menu>
            )}
        </div>
    );
};

export default Place;