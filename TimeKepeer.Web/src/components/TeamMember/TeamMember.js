import React from "react";
import "./TeamMember.css";
function TeamMember() {
    console.log(this.props.icon);
    return (
        <div className="team-member-card">
            <div className="avatar">
                <img src={this.props.icon} alt="description"></img>
            </div>
            <h3>{this.props.name}</h3>
            <h5>{this.props.role}</h5>
            <img
                classNameName="linkendin-icon"
                src="https://image.flaticon.com/icons/svg/174/174857.svg"
                alt="icon"
            ></img>
        </div>
    );
}
export default TeamMember;
