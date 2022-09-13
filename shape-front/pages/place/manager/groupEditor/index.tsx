import { NextPage } from "next";
import { useRouter } from "next/router";
import { ShapeGroup } from "../../../../interfaces/ShapeGroup";

const GroupEditor: NextPage = () => {
    const router = useRouter();

    const shapeGroup: ShapeGroup = (router.query as unknown) as ShapeGroup;

    return <>

    </>;
};

export default GroupEditor;