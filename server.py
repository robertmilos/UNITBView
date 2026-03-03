import http.server
import socketserver

class UnityRequestHandler(http.server.SimpleHTTPRequestHandler):
    def end_headers(self):
        # Mandatory for Unity WebGL to bypass CORS and security blocks
        self.send_header('Access-Control-Allow-Origin', '*')
        self.send_header('Cross-Origin-Embedder-Policy', 'require-corp')
        self.send_header('Cross-Origin-Opener-Policy', 'same-origin')
        super().end_headers()

    def send_head(self):
        path = self.translate_path(self.path)
        # Manually handle the headers for .gz files
        if path.endswith('.gz'):
            ctype = self.guess_type(path[:-3]) # Guess type based on file before .gz
            self.send_response(200)
            self.send_header('Content-Type', ctype)
            self.send_header('Content-Encoding', 'gzip')
            # Important: don't call super().send_head() as it will double-send headers

            f = None
            try:
                f = open(path, 'rb')
            except OSError:
                self.send_error(404, "File not found")
                return None

            self.end_headers()
            return f
        return super().send_head()

PORT = 35691
# Using ThreadingMixIn prevents one slow download from blocking the whole server
class ThreadedHTTPServer(socketserver.ThreadingMixIn, socketserver.TCPServer):
    pass

with ThreadedHTTPServer(("0.0.0.0", PORT), UnityRequestHandler) as httpd:
    print(f"Server active on port {PORT}. External IP: 92.180.20.185")
    httpd.serve_forever()
