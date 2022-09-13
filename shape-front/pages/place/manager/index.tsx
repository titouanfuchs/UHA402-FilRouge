import type { NextPage } from 'next';
import Headerbar from '../../../components/HeaderbarComponent';
import ShapeGroupsManagerComponent from './components/ShapeGroupsManagerComponent';

const Manager: NextPage = () => {
    return (
        <div key="main">
            <Headerbar Title="PlaceManager" key="Header"></Headerbar>
            <div className="p-2">
                <ShapeGroupsManagerComponent></ShapeGroupsManagerComponent>
            </div>

        </div>
    );
};

export default Manager;