import React from 'react';
import {
  TouchableOpacity,
  StyleSheet,
  Text, Image
} from 'react-native';
function ItemTeams({ item, openItem }) {
  const {id,name, description} = item;
  return (
    <TouchableOpacity
      onPress={() => openItem(item)}
      style={[
        styles.item,
        { backgroundColor:  'white' },
      ]}
    >
      <Image
        style={styles.image}
        source={{ uri: 'http://www.newdesignfile.com/postpic/2009/03/employee-benefits-clip-art-icon_303858.png' }}
      />
      <Text style={styles.title}>{name}</Text>
      <Text style={styles.description}>{description}</Text>
     


    </TouchableOpacity>
  );
}

const styles = StyleSheet.create({

  item: {
    backgroundColor: 'lightcyan',
    padding: 20,
    marginVertical: 8,
    marginHorizontal: 16,
    display: 'flex',
    flexDirection: 'row',
    justifyContent: 'space-between',
    marginBottom: 3,
    height: 70
  },
  title: {
    fontSize: 20,
    fontWeight: 'bold',
    position: "absolute",
    top: 10,
    left: 80,
    color: 'black'
  },
  description: {
    fontSize: 14,
    color: 'black',
    position: "absolute",
    top: 40,
    left: 80,
  },
  image: {
    width: 40,
    height: 40,
    marginBottom: 20,
  },
});
export { ItemTeams };