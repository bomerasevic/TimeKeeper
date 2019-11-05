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
import Welcome from "./components/Welcome/Welcome";
import { BrowserRouter, Switch, Route } from "react-router-dom";

class App extends React.Component {
    render() {
        return (
            <BrowserRouter>
                <Switch>
                    <Route exact path="/static">
                        <div className="App">
                            <Home />
                            <AboutUs />
                            <Services />
                            <Slider />
                            <Team />
                            <Contact />
                            <Footer />
                        </div>
                    </Route>
                    <Route exact path="/app">
                        <Welcome />
                    </Route>
                </Switch>
            </BrowserRouter>
        );
    }
}

export default App;
