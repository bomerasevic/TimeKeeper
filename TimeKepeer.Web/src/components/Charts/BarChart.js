import * as V from "victory";
import React from "react";
import data from "./data";
import { VictoryBar, VictoryChart, VictoryAxis, VictoryTheme, VictoryLabel } from "victory";

class BarChart extends React.Component {
    static defaultProps = {
        height: 250,
        width: 250,
        domainPadding: 20,
        horizontal: false,
        data, // {name: "", value: ""} pairs
        angle: 90,
        labelPadding: 25,
        fontSize: 14
    };
    render() {
        const {
            height,
            width,
            domainPadding,
            horizontal,
            data,
            angle,
            labelPadding,
            fontSize,
            xLabel,
            yLabel
        } = this.props;
        return (
            <div
                style={{
                    height: height === "auto" ? height : height + "px",
                    width: width + "px",
                    maxWidth: "100%"
                }}
            >
                <VictoryChart
                    // domainPadding will add space to each side of VictoryBar to
                    // prevent it from overlapping the axis
                    domainPadding={domainPadding}
                    theme={VictoryTheme.material}
                >
                    <VictoryAxis
                        // tickValues specifies both the number of ticks and where
                        // they are placed on the axis
                        tickFormat={data.map(x => x.name)}
                        // tickLabelComponent={<VictoryLabel dx={80} verticalAnchor="middle" />}
                        style={{
                            tickLabels: {
                                angle: angle,
                                padding: labelPadding,
                                fontSize: fontSize
                            }
                        }}
                        label={xLabel}
                    />
                    <VictoryAxis
                        dependentAxis
                        label={yLabel}
                        // tickFormat specifies how ticks should be displayed
                        // tickFormat={x}
                        style={{
                            tickLabels: {
                                // angle: angle,
                                padding: labelPadding,
                                fontSize: fontSize
                            }
                        }}
                    />
                    <VictoryBar
                        animate={{
                            duration: 0,
                            onLoad: { duration: 0 }
                        }}
                        data={data}
                        labels={({ datum }) => `${datum._y}`}
                        x="name"
                        y="value"
                        horizontal={horizontal}
                    />
                </VictoryChart>
            </div>
        );
    }
}

export default BarChart;