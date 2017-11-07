import * as React from 'react';
import { RouteComponentProps } from 'react-router';

export interface Member {
    login: string;
}

export const MemberList = (props: { members: Member[] }) =>
    (
        <ul>
            {
                props.members.map((member) => (<li key={member.login}>{member.login}</li>))
            }
        </ul>
    );