import React, { Component } from "react";
import "./App.css";
import "materialize-css/dist/css/materialize.min.css";
import Home from "./components/Home/Home";
import AboutUs from "./components/AboutUs/AboutUs";
//import services from "./data/services.json";
import Team from "./components/Team/Team";
import Slider from "react-slick";
import Services from "./components/Service/Services";
import Contact from "./components/Contact/Contact";
import Footer from "./components/Footer/Footer";
//import "react-responsive-carousel/lib/styles/carousel.min.css";

class App extends React.Component {
    render() {
        return (
            <div className="App">
                <Home />
                <AboutUs />
                <Services />
                <Slider />
                <Team />
                <Contact />
                <Footer />
            </div>
        );
    }
}

export default App;
