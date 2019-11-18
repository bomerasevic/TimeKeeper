import React, { Component, Fragment } from "react";
import TableTK from "../TableTK/TableTK";
import Grid from "@material-ui/core/Grid";
import Button from "@material-ui/core/Button";
import Container from "@material-ui/core/Container";

function TableView(props) {
  return (
    <Container fixed maxWidth="xl">
      <Grid container justify="center">
        <Grid item xs={12}>
          <Grid container justify="space-between" alignItems="center">
            <h2>{props.title}</h2>
            <Button
              className=" btn modal-trigger add-btn"
              variant="contained"
              color="primary"
              onClick={() => props.handleClickOpen(null)}
            >
              Add
            </Button>
          </Grid>
        </Grid>
        <Grid item xs={12}>
          {/* <TableTK head={this.state.head} rows={this.state.rows}/> */}
          <TableTK {...props.table} handleClickOpen={props.handleClickOpen} />
        </Grid>
      </Grid>
    </Container>
  );
}
export default TableView;