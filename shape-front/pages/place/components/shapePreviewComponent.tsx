import {BaseShape} from "../../../interfaces/BaseShape";
import {useEffect, useState} from "react";
import {ShapeGroup} from "../../../interfaces/ShapeGroup";

type shapePreviewComponentT = {
    shapeId: number
}
const shapePreviewComponent = ({shapeId}:shapePreviewComponentT)=>{

    const [group , setGroup] = useState<ShapeGroup|undefined>(undefined);
    
    useEffect(() => {
        fetch(`/shapeAPI/api/Shape/Group/${shapeId}`)
        .then((res) => res.json())
        .then((data) => {
            setGroup(data);
            console.log(data);
        });
    }, []);
    
    return (
        <div className="aspect-square border w-20 hover:scale-150 hover:z-[10] transition-all bg-white hover:shadow-lg ease-in-out cursor-pointer">
            {group?.groupName}
        </div>
    )
}

export default shapePreviewComponent;