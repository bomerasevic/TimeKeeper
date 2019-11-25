import React from 'react';

import { withStyles } from '@material-ui/core/styles';
import Card from '@material-ui/core/Card';

import CardContent from '@material-ui/core/CardContent';

import Typography from '@material-ui/core/Typography';

const styles = {
  card: {
    width: '90%',
    margin: 'auto',
    height: 255,
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'center'
  }
};

function TeamDescription(props) {
  const { classes } = props;
  

  return (
    <Card className={classes.card}>
      <CardContent>
        
        <Typography  variant="h5" component="h2" >
        
        </Typography>
       
      </CardContent>
      
    </Card>
  );
}


export default withStyles(styles)(TeamDescription);