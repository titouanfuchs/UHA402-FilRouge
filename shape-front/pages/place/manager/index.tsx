import type { NextPage } from 'next';
import Headerbar from '../../../components/HeaderbarComponent';
import ShapeGroupsManagerComponent from './components/ShapeGroupsManagerComponent';
import ShapeManagerComponent from './components/ShapesManagerComponent';

const Manager: NextPage = () => {
    return (
        <div key="main">
            <Headerbar Title="PlaceManager" key="Header"></Headerbar>
            <div className="p-2 space-y-5">
                <ShapeGroupsManagerComponent></ShapeGroupsManagerComponent>
                <ShapeManagerComponent></ShapeManagerComponent>
            </div>

        </div>
    );
};

export default Manager;