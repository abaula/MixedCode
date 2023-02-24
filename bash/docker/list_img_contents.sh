#!/bin/bash

IMG_NAME=$1
OUT_FILE_PATH=$2

echo "This will list the file-system content of image '$IMG_NAME' to '$OUT_FILE_PATH' file."
echo "  Image => $IMG_NAME"
echo "  Output file => $OUT_FILE_PATH"

read -p "Do you want to proceed? (yes/no) " yn

case $yn in
	yes ) echo ok, we will proceed;;
	no ) echo exiting...;
		exit;;
	* ) echo invalid response;
		exit 1;;
esac

docker create --name="tmp_$$" "$IMG_NAME"  > /dev/null
docker export tmp_$$ | tar t > "$OUT_FILE_PATH"
docker rm tmp_$$  > /dev/null