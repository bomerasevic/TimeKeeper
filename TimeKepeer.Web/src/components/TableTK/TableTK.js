import React from "react";
import Table from "@material-ui/core/Table";
import TableBody from "@material-ui/core/TableBody";
import TableCell from "@material-ui/core/TableCell";
import TableHead from "@material-ui/core/TableHead";
import TableRow from "@material-ui/core/TableRow";
import Paper from "@material-ui/core/Paper";
import Button from "@material-ui/core/Button";
import { connect } from "react-redux";
import { loadEmployees } from "../../store/actions";
import "./TableTK.css";


function TableTK(props) {
  return (
    <Paper elevation={3}>
      <Table className="table-tk">
        <TableHead style={{ backgroundColor: "rgb(57, 54, 67, 0.9)" }}>
          <TableRow className="table-tk-row-dark">
            {props.head.map((cell, i) => (
              <TableCell style={{ color: "white" }} key={i}>{cell}</TableCell>
            ))}
            {props.actions && <TableCell style={{ color: "white" }}>Actions</TableCell>}
          </TableRow>
        </TableHead>
        <TableBody>
          {props.rows.map((row, i) => (
            <TableRow key={i}>
              {Object.keys(row).map((cell, i) => {
                return <TableCell key={i}>{row[cell]}</TableCell>;
              })}
              {props.actions && (
                <TableCell>
                  <Button onClick={() => props.handleClickOpen(row.id)}>
                    Edit
                  </Button>
                </TableCell>
              )}
            </TableRow>
          ))}
        </TableBody>
      </Table>

    </Paper>
  );
}

export default TableTK;

