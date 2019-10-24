import React from "react";
import "./TeamMember.css";
class TeamMember extends React.Component {
    render() {
        console.log(this.props.icon);
        return (
            <div class="team-member-card">
                <div class="avatar">
                    <img src={this.props.icon} alt="description"></img>
                </div>
                <h3>{this.props.name}</h3>
                <h5>{this.props.role}</h5>
                <img
                    className="linkendin-icon"
                    src="https://image.flaticon.com/icons/svg/174/174857.svg"
                    alt="icon"
                ></img>
            </div>
        );
    }
}
export default TeamMember;
