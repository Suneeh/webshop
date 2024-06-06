# 1. Login to the server 
### NOTE: sftp does not accept passwords in scripts - but you can whitelist your IP in the server settings
### You can also use sshpass or lftp to automate the process including your password
# 2. Potentially clean your httpdocs folder before uploading the new files
# 3. Upload the files to the server
sftp REMOTE_USERNAME@SERVER_IP <<EOF
put ./dist/loldle/* /YOUR_PATH