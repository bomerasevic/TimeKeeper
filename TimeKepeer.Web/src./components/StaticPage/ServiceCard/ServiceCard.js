import React from "react";
import "./ServiceCard.css";
class ServiceCard extends React.Component {
    render() {
        console.log(this.props.serviceTitle);
        return (
            <div className="col s12 m4 padding-right">
                <h5>{this.props.serviceTitle}</h5>
                <p>{this.props.serviceDescription}</p>
            </div>
        );
    }
}
export default ServiceCard;
