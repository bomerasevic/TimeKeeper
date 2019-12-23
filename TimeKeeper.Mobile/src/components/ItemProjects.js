import React from 'react';
import {
  TouchableOpacity,
  StyleSheet,
  Text, Image
} from 'react-native';
function ItemProjects({ item, openItem }) {
  const {id,name,description, amount} = item;
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
        source={{ uri: 'https://www.future-science.com/pb-assets/authorguide%20assets/Icons/Makingthemost-1534941955143.png' }}
      />
      <Text style={styles.title}>{name}</Text>
      <Text style={styles.description}>{amount}</Text>
     


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
export { ItemProjects };