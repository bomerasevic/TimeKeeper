import React from "react";
import "./UserCard/UserCard.css";

import axios from "axios";
import classNames from "classnames";
import Slider from "react-slick";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
import { withStyles } from "@material-ui/core/styles";
import { Backdrop } from "@material-ui/core";
//import styles from "../Styles/TableStyles";
import config from "../../../config";
import TeamCard from "./TeamCard/TeamCard";
import CircularProgress from "@material-ui/core/CircularProgress";
import TeamDescription from "./TeamDescription/TeamDescription";
import UserCard from "./UserCard/UserCard";
import NavigationLogin from "../../NavigationLogin/NavigationLogin";
import Container from '@material-ui/core/Container';
class TeamsPage extends React.Component {
    state = {
        loading: false,
        data: null,
        selectedTeamId: null,
        selectedTeam: null
    };
    componentDidMount() {
        this.setState({ loading: true });
        axios(`${config.apiUrl}teams`, {
            headers: {
                "Content-Type": "application/json",
                Authorization: config.token
            }
        })
            .then(res => {
                this.setState({ data: res.data, loading: false });
            })
            .catch(err => this.setState({ loading: false }));
    }
    handleClick = id => {
        axios(`${config.apiUrl}teams/${id}`, {
            headers: {
                "Content-Type": "application/json",
                Authorization: config.token
            }
        })
            .then(res => {
                console.log(res);
                this.setState({ selectedTeam: res.data });
            })
            .catch(err => { });
    };
    render() {
        const { classes } = this.props;
        const { loading, data } = this.state;
        let settings = {
            dots: true,
            infinite: true,
            speed: 1000,
            slidesToShow: 3,
            slidesToScroll: 1
        };

        return (
            <React.Fragment>
                {loading ? (
                    <Backdrop open={loading}>
                        <div className="classes.center">
                            <CircularProgress size={100} className="classes.loader" />
                            <h1 className="classes.loaderText">Loading...</h1>
                        </div>
                    </Backdrop>
                ) : data ? (
                    <div>
                        <NavigationLogin />
                        <Container
                            style={{

                                marginBottom: "5px",
                                margin: " auto",
                                marginTop: "28px"
                            }}
                        >

                            <Slider  {...settings}>
                                {data.map(d => (
                                    <div
                                        key={d.id}
                                        id={d.id}
                                        name={d.name}
                                        description={d.description}
                                        onClick={() => this.handleClick(d.id)}
                                    >
                                        <TeamCard
                                            key={d.id}
                                            id={d.id}
                                            name={d.name}
                                            description={d.description}
                                            onCLick={() => this.handleClick(d.id)}
                                        />
                                    </div>

                                ))}
                            </Slider>
                        </Container>
                        <Container
                            style={{

                                //margin: " 0 180px",
                                marginTop: "36px"
                            }}
                        >

                            <Slider className="members" {...settings}>
                                {this.state.selectedTeam
                                    ? this.state.selectedTeam.members.map(s => (
                                        <UserCard className="userCard" key={s.id} name={s.name}></UserCard>
                                    ))
                                    : null}{" "}
                            </Slider>

                        </Container>

                    </div>
                ) : null}
            </React.Fragment>
        );
    }
}
export default TeamsPage;