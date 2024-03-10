tag=$(date +"%Y%m%d%H%M")

docker build -t rinha-de-backend:$tag -t luc4skr/rinha-de-backend:$tag .
docker push luc4skr/rinha-de-backend:$tag

echo $tag
