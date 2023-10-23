# Generated by the gRPC Python protocol compiler plugin. DO NOT EDIT!
"""Client and server classes corresponding to protobuf-defined services."""
import grpc

from . import services_pb2 as services__pb2


class ImageInterrogatorStub(object):
    """Missing associated documentation comment in .proto file."""

    def __init__(self, channel):
        """Constructor.

        Args:
            channel: A grpc.Channel.
        """
        self.ListInterrogators = channel.unary_unary(
                '/interrogator.ImageInterrogator/ListInterrogators',
                request_serializer=services__pb2.InterrogatorListingRequest.SerializeToString,
                response_deserializer=services__pb2.InterrogatorListing.FromString,
                )
        self.InterrogateImage = channel.unary_unary(
                '/interrogator.ImageInterrogator/InterrogateImage',
                request_serializer=services__pb2.InterrogationRequest.SerializeToString,
                response_deserializer=services__pb2.ImageTagResults.FromString,
                )


class ImageInterrogatorServicer(object):
    """Missing associated documentation comment in .proto file."""

    def ListInterrogators(self, request, context):
        """Missing associated documentation comment in .proto file."""
        context.set_code(grpc.StatusCode.UNIMPLEMENTED)
        context.set_details('Method not implemented!')
        raise NotImplementedError('Method not implemented!')

    def InterrogateImage(self, request, context):
        """Missing associated documentation comment in .proto file."""
        context.set_code(grpc.StatusCode.UNIMPLEMENTED)
        context.set_details('Method not implemented!')
        raise NotImplementedError('Method not implemented!')


def add_ImageInterrogatorServicer_to_server(servicer, server):
    rpc_method_handlers = {
            'ListInterrogators': grpc.unary_unary_rpc_method_handler(
                    servicer.ListInterrogators,
                    request_deserializer=services__pb2.InterrogatorListingRequest.FromString,
                    response_serializer=services__pb2.InterrogatorListing.SerializeToString,
            ),
            'InterrogateImage': grpc.unary_unary_rpc_method_handler(
                    servicer.InterrogateImage,
                    request_deserializer=services__pb2.InterrogationRequest.FromString,
                    response_serializer=services__pb2.ImageTagResults.SerializeToString,
            ),
    }
    generic_handler = grpc.method_handlers_generic_handler(
            'interrogator.ImageInterrogator', rpc_method_handlers)
    server.add_generic_rpc_handlers((generic_handler,))


 # This class is part of an EXPERIMENTAL API.
class ImageInterrogator(object):
    """Missing associated documentation comment in .proto file."""

    @staticmethod
    def ListInterrogators(request,
            target,
            options=(),
            channel_credentials=None,
            call_credentials=None,
            insecure=False,
            compression=None,
            wait_for_ready=None,
            timeout=None,
            metadata=None):
        return grpc.experimental.unary_unary(request, target, '/interrogator.ImageInterrogator/ListInterrogators',
            services__pb2.InterrogatorListingRequest.SerializeToString,
            services__pb2.InterrogatorListing.FromString,
            options, channel_credentials,
            insecure, call_credentials, compression, wait_for_ready, timeout, metadata)

    @staticmethod
    def InterrogateImage(request,
            target,
            options=(),
            channel_credentials=None,
            call_credentials=None,
            insecure=False,
            compression=None,
            wait_for_ready=None,
            timeout=None,
            metadata=None):
        return grpc.experimental.unary_unary(request, target, '/interrogator.ImageInterrogator/InterrogateImage',
            services__pb2.InterrogationRequest.SerializeToString,
            services__pb2.ImageTagResults.FromString,
            options, channel_credentials,
            insecure, call_credentials, compression, wait_for_ready, timeout, metadata)
