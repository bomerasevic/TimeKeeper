import React from "react";
import "./Service.css";
import Slider from "react-slick";
import ServiceCard from "./../ServiceCard/ServiceCard";
import Service from "../../../data/services.json";
class Services extends React.Component {
    render() {
        const serviceCards = Service.map((card, i) => (
            <ServiceCard
                key={i}
                serviceTitle={card.serviceTitle}
                serviceDescription={card.serviceDescription}
            />
        ));
        return (
            <div className="gradient" id="services">
                <div className="container">
                    <div className="row">
                        <h2 className="service-header">Services</h2>
                        <h3 className="service-header3">
                            Our service saves time but goes way beyond time
                        </h3>
                    </div>
                    <div className="row features">{serviceCards}</div>
                </div>
            </div>
        );
    }
}
export default Services;
