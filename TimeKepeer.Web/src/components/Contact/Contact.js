import React, { Component } from "react";
import "./Contact.css";


function Contact(){


   return (
    <div className="contact">
    <div className="container">
      <div className="row">
        <div className="col m4 s12">
          <h6>TIME IS RELATIVE</h6>
          <h3>Contact us.</h3>
          <h5>We like to improve</h5>
        </div>
        <div className="col m4 s12">
          <div className="input-field ">
            <input id="first_name" type="text" className="validate" />
            <label for="first_name">First Name</label>
          </div>
          <div className="input-field">
            <input id="email" type="email" className="validate" />
            <label for="email">Email</label>
          </div>
          <div className="input-field">
            <input id="icon_telephone" type="tel" className="validate" />
            <label for="icon_telephone">Telephone</label>
          </div>
        </div>
        <div className="col m4 s12">
          <div className="input-field">
            <textarea id="textarea2" className="materialize-textarea" data-length="250"></textarea>
            <label for="textarea2">Message</label>
          </div>
          <a className="waves-effect waves-light btn">Send Message</a>
        </div>
      </div>
    </div>
  </div>



   );



}

export default Contact;