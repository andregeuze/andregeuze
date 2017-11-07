import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { DateRangePicker, SingleDatePicker, DayPickerRangeController, SingleDatePickerShape } from 'react-dates';
import * as moment from 'moment';
import 'react-dates/lib/css/_datepicker.css';
import { Member, MemberList } from './MemberList';

interface RijbewijsBlockState {
    rijbewijsnummer: string;
    landVanAfgifte: string;
    einddatum: moment.Moment;
    focused: boolean;
    response: any;
    members: Member[];
}

interface FocusChange {
    focused: boolean | null;
}

export class RijbewijsBlock extends React.Component<{}, RijbewijsBlockState> {
    constructor() {
        super();
        this.state = { rijbewijsnummer: "", landVanAfgifte: "", einddatum: moment(), focused: false, response: null, members: [] };
    }

    handleInputChange = (event: React.ChangeEvent<any>) => {
        const target = event.target;
        const value = target.type === 'checkbox' ? target.checked : target.value;
        const name = target.name;

        this.setState({
            [name]: value
        });
    }

    handleRijbewijsnummerChange = (rijbewijsnummer: React.ChangeEvent<any>) => {
        this.setState({
            rijbewijsnummer: rijbewijsnummer.target.value
        });

        // do api call
        let baseURL = 'https://api.github.com/orgs/lemoncode';
        let membersURL = `${baseURL}/members`;

        fetch(membersURL)
            .then((response) => {
                let json = response.json();
                console.log(json);

                json.then((members) => {
                    this.setState({
                        members: members
                    });
                })
            });
    }

    handleDateChange = (date: moment.Moment): void => {
        this.setState({
            einddatum: date
        });
    }

    handleFocusChange = ({ focused }: FocusChange): void => {
        this.setState({
            focused: focused === true
        });
    }

    public render() {
        return <div>
            <h1>Rijbewijsgegevens</h1>

            <p>
                <label>
                    Rijbewij(snummer): <input name="rijbewijsNummer" type="text" value={this.state.rijbewijsnummer} onChange={this.handleRijbewijsnummerChange} />
                </label>

                <label>{this.state.response}</label>
            </p>

            <MemberList members={this.state.members} />

            <label>
                Land van afgifte: <input name="landVanAfgifte" type="text" value={this.state.landVanAfgifte} onChange={this.handleInputChange} />
            </label>

            <SingleDatePicker
                id="einddatum_input"
                date={this.state.einddatum}
                focused={this.state.focused}
                onDateChange={this.handleDateChange}
                onFocusChange={this.handleFocusChange}
            />
        </div>;
    }
}
