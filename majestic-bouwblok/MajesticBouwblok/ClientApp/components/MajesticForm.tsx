import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { RijbewijsBlock } from './blocks/RijbewijsBlock';

export class MajesticForm extends React.Component<RouteComponentProps<{}>, {}> {

    public render() {
        return <div>
            <h1>Hello, majestic world!</h1>

            <RijbewijsBlock />
        </div>;
    }


}
