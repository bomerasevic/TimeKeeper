import React, { Component } from "react";
import logo from "./logo.svg";
import "./App.css";
import Home from "./components/Home/Home";
import AboutUs from "./components/AboutUs/AboutUs";
//import services from "./data/services.json";
import Team from "./components/Team/Team";
import Services from "./components/Service/Services";
import Contact from "./components/Contact/Contact";
import Footer from "./components/Footer/Footer";

class App extends React.Component {
    render() {
        return (
            <div className="App">
                <Home />
                <AboutUs />
                <Services />
				<Team />
                <Contact />
                <Footer />
            </div>
        );
    }
}

export default App;
