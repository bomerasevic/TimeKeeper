import * as V from "victory";
import React from "react";
import data from "./data";

import { VictoryPie, VictoryChart, VictoryAxis, VictoryTheme, VictoryLabel } from "victory";

// const data = [
//   { quarter: 1, earnings: 13000 },
//   { quarter: 2, earnings: 16500 },
//   { quarter: 3, earnings: 14250 },
//   { quarter: 4, earnings: 19000 }
// ];

// const modifiedData = data.missingEntries.map(x => {
//   return {
//     team: x.team.name,
//     value: x.value
//   };
// });

// [
//   { x: "Worked", y: data.baseTotalHours },
//   { x: "Total", y: data.totalHours }
// ]

class PieChart extends React.Component {
    state = {
        maxWidth: null
    };
    static defaultProps = {
        height: 250,
        width: 250,
        padding: 50,
        innerRadius: 75,
        fontSize: 24,
        padAngle: 0,
        data,
        title: "Chart"
    };
    render() {
        const { height, width, padding, innerRadius, fontSize, padAngle, data, title } = this.props;
        return (
            <div
                className="pie-charts"
                style={{
                    position: "relative"
                }}
            >
                <VictoryPie

                    colorScale={["#1d7a72", "black"]}
                    padding={padding}
                    innerRadius={innerRadius}
                    labels={({ datum }) => ` ${datum.y}`}
                    style={{
                        labels: {
                            fontSize: fontSize
                        }
                    }}
                    padAngle={padAngle}
                    data={data}
                    animate={{
                        duration: 0,
                        onLoad: {
                            duration: 0,
                            before: () => ({ _y: -1200, label: " " }),
                            after: datum => ({ _y: datum._y })
                        }
                    }}
                />
                <h6
                    style={{
                        // position: "absolute",
                        // top: "50%",
                        // right: "50%",
                        // transform: " translate(50%,-50%)",
                        textAlign: "center",
                        fontSize: "16px"
                        // maxWidth: innerRadius
                    }}
                >
                    {title}
                </h6>
            </div>
        );
    }
}

export default PieChart;